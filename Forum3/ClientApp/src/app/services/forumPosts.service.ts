import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {ForumPost} from "../models/forumPost.model";

@Injectable({
  providedIn: 'root'
})
export class ForumPostsService {

  constructor(private http: HttpClient) { }

  getAllPostsOfThread(id:number):Observable<ForumPost[]>{
    return this.http.get<ForumPost[]>("api/ForumPost/" + id)
  }
}
