import { Component, OnInit } from "@angular/core";
import { ForumCategoriesService } from "../services/forum-categories.service";
import { ForumCategory } from "../models/forum-category.model";

@Component({
  selector: 'app-forum-categories-component',
  templateUrl: './forum-categories.component.html'
})
export class ForumCategoriesComponent implements OnInit{
  isLoading: boolean = true;
  isError: boolean = false;

  categories: ForumCategory[] = [];
  constructor(private categoriesServices: ForumCategoriesService) { }

  ngOnInit(): void {
    this.categoriesServices.getAllCategories().subscribe({
      next:(categories) => {
        this.categories = categories;
        this.isLoading = false;
      },
      error:(response) =>{
        this.isError = true;
        this.isLoading = false;
        console.log(response);
      }
    });
  }
}
