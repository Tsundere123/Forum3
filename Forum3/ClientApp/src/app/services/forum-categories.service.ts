import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ForumCategory } from "../models/forum-category.model";

@Injectable({
  providedIn: 'root'
})
export class ForumCategoriesService {
  constructor(private http: HttpClient) { }
  getAllCategories():Observable<ForumCategory[]>{
    return this.http.get<ForumCategory[]>( "api/ForumCategory")
  }
}
