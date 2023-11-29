import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Search } from "../models/search.model";
import { LookupThread } from "../models/lookup/lookup-thread.model";
import { LookupPost } from "../models/lookup/lookup-post.model";
import { LookupMember } from "../models/lookup/lookup-member.model";

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private http: HttpClient) { }

  searchIndex(query: string) {
    return this.http.get<Search>('api/Search?query=' + query);
  }

  searchThreads(query: string) {
    return this.http.get<LookupThread[]>('api/Search/threads?query=' + query);
  }

  searchPosts(query: string) {
    return this.http.get<LookupPost[]>('api/Search/posts?query=' + query);
  }

  searchMembers(query: string) {
    return this.http.get<LookupMember[]>('api/Search/members?query=' + query);
  }
}
