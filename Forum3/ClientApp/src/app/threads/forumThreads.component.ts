import {Component, OnInit} from "@angular/core";
import {ForumCategoriesService} from "../services/forumCategories.service";
import {Category} from "../models/forumCategory.model";
import {ForumThreadsService} from "../services/forumThreads.service";
import {Thread} from "../models/forumThread.model";
import {ActivatedRoute} from "@angular/router";
import {ForumCategoriesComponent} from "../categories/forumCategories.component";
import {inject} from "@angular/core/testing";

@Component({
  selector: 'app-forumThreads-component',
  templateUrl: './forumThreads.component.html',
  styleUrls: ['./forumThreads.component.css']
})

export class ForumThreadsComponent implements OnInit{
  threadsInCategory: Thread[] = [];
  currentCategoryName: string = "";
  categoryId:number = 0;
  constructor(private threadsServices: ForumThreadsService, private activatedRoute:ActivatedRoute) { }
  ngOnInit(): void {

    // this.categoryId = this.activatedRoute.snapshot.paramMap.get('id');
    // this.service.threads.find()

    // this.threadsServices.getCategoryName().subscribe({
    //   next:(categories) =>{
    //     categories.forEach(function(name){
    //       console.log(name);
    //     });
    //     console.log (categories);
    //     // this.currentCategoryName = categories;
    //   }
    // })

    this.activatedRoute.params.subscribe(params => this.categoryId = +params['id']);

    this.threadsServices.getAllThreadsOfCategory(this.categoryId).subscribe({
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
