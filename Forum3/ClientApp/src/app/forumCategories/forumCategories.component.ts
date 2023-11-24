import {Component, OnInit} from "@angular/core";
import {ForumCategoriesService} from "../services/forumCategories.service";
import {Category} from "../models/forumCategory.model";
import {Thread} from "../models/forumThread.model";
import {CategoryThreadCount} from "../models/forumCategoryThreadCount.model";
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-forumCategories-component',
  templateUrl: './forumCategories.component.html',
  styleUrls: ['./forumCategories.component.css']
})

export class ForumCategoriesComponent implements OnInit{
  categories: Category[] = [];
  // nonSoftDeletedThreadsCount: CategoryThreadCount[] = [];
  constructor(private categoriesServices: ForumCategoriesService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.categoriesServices.getAllCategories().subscribe({
      next:(categories) => {
        console.log(categories);
        this.categories = categories;
      },
      error:(response) =>{
        console.log(response);
      }
    });

    // this.categoriesServices.getNumberOfThreadsByCategoryId.subscribe({
    //   next:(nonSoftDeletedThreadsCount) => {
    //     console.log(nonSoftDeletedThreadsCount);
    //     this.categories = nonSoftDeletedThreadsCount;
    //   },
    //   error:(response) =>{
    //     console.log(response);
    //   }
    // });
  }
}
