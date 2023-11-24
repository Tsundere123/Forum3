import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {Category} from "../models/forumCategory.model";
import {Thread} from "../models/forumThread.model";
import {CategoryThreadCount} from "../models/forumCategoryThreadCount.model";

@Injectable({
  providedIn: 'root'
})
export class ForumCategoriesService {

  // baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }
  getAllCategories():Observable<Category[]>{
    return this.http.get<Category[]>( "api/ForumCategory")
  }
  // getNumberOfThreadsByCategoryId():Observable<CategoryThreadCount[]>{
  //   return this.http.get<Category[]>("api/Category{id}")
  // }
}
