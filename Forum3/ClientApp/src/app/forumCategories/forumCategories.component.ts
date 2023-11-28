import { Component, OnInit } from "@angular/core";
import { ForumCategoriesService } from "../services/forumCategories.service";
import { ForumCategory } from "../models/forumCategory.model";

@Component({
  selector: 'app-forumCategories-component',
  templateUrl: './forumCategories.component.html',
  styleUrls: ['./forumCategories.component.css']
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
