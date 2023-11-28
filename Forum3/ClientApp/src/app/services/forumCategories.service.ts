import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {ForumCategory} from "../models/forum-category.model";
import {ForumThread} from "../models/forum-thread/forum-thread.model";
import {CategoryThreadCount} from "../models/forum-category-thread-count.model";

@Injectable({
  providedIn: 'root'
})
export class ForumCategoriesService {
  constructor(private http: HttpClient) { }
  getAllCategories():Observable<ForumCategory[]>{
    return this.http.get<ForumCategory[]>( "api/ForumCategory")
  }
}
