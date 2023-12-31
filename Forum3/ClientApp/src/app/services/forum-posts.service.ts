import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ForumPost } from "../models/forum-post.model";

@Injectable({
  providedIn: 'root'
})
export class ForumPostsService {

  constructor(private http: HttpClient) { }

  GetAllPostsOfThread(id:number):Observable<ForumPost[]>{
    return this.http.get<ForumPost[]>("api/ForumPost/" + id);
  }

  CreatePost(threadId: number, newPost: any){
    return this.http.post<any>("api/ForuMPost/Create/" + threadId, newPost);
  }
  EditCurrentPost(postId: number, newContent :any){
    return this.http.post<any>("api/ForumPost/Edit/" + postId, newContent);
  }

  PermaDeleteCurrentPost(postId: number){
    return this.http.delete<any>("api/ForumPost/PermaDelete/" + postId);
  }
  SoftDeleteCurrentPost(postId: number){
    return this.http.delete<any>("api/ForumPost/SoftDelete/" + postId);
  }
  UnSoftDeleteCurrentPost(postId: number){
    return this.http.delete<any>("api/ForumPost/UnSoftDelete/" + postId);
  }
}
