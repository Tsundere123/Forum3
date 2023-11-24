import {Component, OnInit} from "@angular/core";
import {ForumCategoriesService} from "../services/forumCategories.service";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThreadsService} from "../services/forumThreads.service";
import {ForumThread} from "../models/forumThread.model";
import {ActivatedRoute} from "@angular/router";
import {ForumCategoriesComponent} from "../forumCategories/forumCategories.component";
import {inject} from "@angular/core/testing";

@Component({
  selector: 'app-forumThreads-component',
  templateUrl: './forumThreads.component.html',
  styleUrls: ['./forumThreads.component.css']
})

export class ForumThreadsComponent implements OnInit{
  threadsInCategory: ForumThread[] = [];
  currentCategoryName: string = "";
  categoryId:number = 0;
  constructor(private forumThreadsServices: ForumThreadsService, private activatedRoute:ActivatedRoute) { }
  ngOnInit(): void {

    // this.categoryId = this.activatedRoute.snapshot.paramMap.get('id');
    // this.service.threads.find()

    // this.forumThreadsServices.getCategoryName().subscribe({
    //   next:(categories) =>{
    //     categories.forEach(function(name){
    //       console.log(name);
    //     });
    //     console.log (categories);
    //     // this.currentCategoryName = categories;
    //   }
    // })

    this.activatedRoute.params.subscribe(params => this.categoryId = +params['id']);

    this.forumThreadsServices.getAllThreadsOfCategory(this.categoryId).subscribe({
      next:(threads) => {
        console.log(threads);
        this.threadsInCategory = threads;
      },
      error:(response) =>{
        console.log(response);
      }
    });
  }

  protected readonly ForumCategoriesComponent = ForumCategoriesComponent;
  protected readonly ForumCategoriesService = ForumCategoriesService;
  protected readonly ForumThreadsService = ForumThreadsService;
}
