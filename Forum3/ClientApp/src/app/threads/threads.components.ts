import {Component, OnInit} from "@angular/core";
import {CategoriesService} from "../services/categories.service";
import {Category} from "../models/category.model";

@Component({
  selector: 'app-threads-component',
  templateUrl: './threads.component.html',
  styleUrls: ['./threads.component.css']
})

export class ThreadsComponent implements OnInit{
  categories: Category[] = [];
  constructor(private categoriesServices: CategoriesService) { }
  ngOnInit(): void {
    this.categoriesServices.getAllThreadsOfCategory().subscribe({
      next:(threads) => {
        console.log(threads);
        this.categories = threads;
      },
      error:(response) =>{
        console.log(response);
      }
    });
  }
}
