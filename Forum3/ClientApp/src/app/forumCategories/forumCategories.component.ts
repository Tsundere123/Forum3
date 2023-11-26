import {Component, OnInit} from "@angular/core";
import {ForumCategoriesService} from "../services/forumCategories.service";
import {ForumCategory} from "../models/forumCategory.model";

@Component({
  selector: 'app-forumCategories-component',
  templateUrl: './forumCategories.component.html',
  styleUrls: ['./forumCategories.component.css']
})

export class ForumCategoriesComponent implements OnInit{
  categories: ForumCategory[] = [];
  constructor(private categoriesServices: ForumCategoriesService) { }

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
  }
}
