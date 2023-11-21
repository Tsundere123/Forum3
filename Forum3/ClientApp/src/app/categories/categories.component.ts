import {Component, OnInit} from "@angular/core";
import {CategoriesService} from "../services/categories.service";
import {Category} from "../models/category.model";

@Component({
  selector: 'app-categories-component',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})

export class CategoriesComponent implements OnInit{
  categories: Category[] = [];
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
  }
}
