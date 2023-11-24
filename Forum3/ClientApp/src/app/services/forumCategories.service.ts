import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThread} from "../models/forumThread.model";
import {CategoryThreadCount} from "../models/forumCategoryThreadCount.model";

@Injectable({
  providedIn: 'root'
})
export class ForumCategoriesService {

  // baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }
  getAllCategories():Observable<ForumCategory[]>{
    return this.http.get<ForumCategory[]>( "api/ForumCategory")
  }
  // getNumberOfThreadsByCategoryId():Observable<CategoryThreadCount[]>{
  //   return this.http.get<Category[]>("api/Category{id}")
  // }
}
