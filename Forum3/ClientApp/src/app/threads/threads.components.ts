import {Component, OnInit} from "@angular/core";
import {CategoriesService} from "../services/categories.service";
import {Category} from "../models/forumCategory.model";
import {ThreadsService} from "../services/threads.service";
import {Thread} from "../models/forumThread.model";

@Component({
  selector: 'app-threads-component',
  templateUrl: './threads.component.html',
  styleUrls: ['./threads.component.css']
})

export class ThreadsComponent implements OnInit{
  threadsInCategory: Thread[] = [];
  constructor(private threadsServices: ThreadsService) { }
  ngOnInit(): void {
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
}
