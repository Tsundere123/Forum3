import {Component, OnInit} from "@angular/core";
import {ForumCategoriesService} from "../services/forumCategories.service";
import {Category} from "../models/forumCategory.model";
import {ThreadsService} from "../services/threads.service";
import {Thread} from "../models/forumThread.model";
import {ActivatedRoute} from "@angular/router";
import {ForumCategoriesComponent} from "../categories/forumCategories.component";

@Component({
  selector: 'app-forumThreads-component',
  templateUrl: './forumThreads.component.html',
  styleUrls: ['./forumThreads.component.css']
})

export class ForumThreadsComponent implements OnInit{
  threadsInCategory: Thread[] = [];
  // categoryId:number;
  constructor(private threadsServices: ThreadsService,private activatedRoute:ActivatedRoute) { }
  ngOnInit(): void {

    // this.categoryId = this.activatedRoute.snapshot.paramMap.get('id');
    // this.service.threads.find()

    this.threadsServices.getAllThreadsOfCategory().subscribe({
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
}
