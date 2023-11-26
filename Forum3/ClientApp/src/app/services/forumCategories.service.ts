import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ForumCategory} from "../models/forumCategory.model";
import {ForumThread} from "../models/forumThread/forumThread.model";
import {CategoryThreadCount} from "../models/forumCategoryThreadCount.model";

@Injectable({
  providedIn: 'root'
})
export class ForumCategoriesService {
  constructor(private http: HttpClient) { }
  getAllCategories():Observable<ForumCategory[]>{
    return this.http.get<ForumCategory[]>( "api/ForumCategory")
  }
}
