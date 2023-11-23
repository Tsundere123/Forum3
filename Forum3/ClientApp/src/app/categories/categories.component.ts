import {Component, OnInit} from "@angular/core";
import {CategoriesService} from "../services/categories.service";
import {Category} from "../models/forumCategory.model";
import {Thread} from "../models/forumThread.model";
import {CategoryThreadCount} from "../models/forumCategoryThreadCount.model";

@Component({
  selector: 'app-categories-component',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})

export class CategoriesComponent implements OnInit{
  categories: Category[] = [];
  nonSoftDeletedThreadsCount: CategoryThreadCount[] = [];
  constructor(private categoriesServices: CategoriesService) { }
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
