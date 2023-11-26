import { BrowserModule } from '@angular/platform-browser';
import {ApplicationConfig, NgModule} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { ForumCategoriesComponent } from "./forumCategories/forumCategories.component";
import { ForumThreadsComponent } from "./forumThreads/forumThreads.component";
import { ForumPostsComponent } from "./forumPosts/forumPosts.component";
import { ConvertToReadableDate } from "./shared/convertToReadableDate";
import { LimitStringLength } from "./shared/limitStringLength";
import { ForumPostCardComponent } from "./forumPosts/forum-post-card/forum-post-card.component";
import { LookupMemberComponent } from "./shared/lookup/lookup-member/lookupMember.component";
import { LookupThreadComponent } from "./shared/lookup/lookup-threads/lookupThread.component";
import { LookupPostComponent } from "./shared/lookup/lookup-post/lookupPost.component";
import { SearchThreadsComponent } from "./search/search-threads/searchThreads.component";
import { SearchPostsComponent } from "./search/search-posts/searchPosts.component";
import { SearchMembersComponent } from "./search/search-members/searchMembers.component";
import { SearchComponent } from "./search/search.component";
import {ForumThreadListItemComponent} from "./forumThreads/forum-thread-list-item/forum-thread-list-item.component";
import {
    ForumCategoryListItemComponent
} from "./forumCategories/forum-category-list-item/forum-category-list-item.component";
import {
  ForumThreadListNewestSoftDeletedComponent
} from "./forumThreads/forum-thread-list-item/forum-thread-list-newest-soft-deleted/forum-thread-list-newest-soft-deleted.component";
import {
  ForumThreadListNormalIconsComponent
} from "./forumThreads/forum-thread-list-item/forum-thread-list-normal-icons/forum-thread-list-normal-icons.component";
import {
  ForumThreadListNoPostsComponent
} from "./forumThreads/forum-thread-list-item/forum-thread-list-no-posts/forum-thread-list-no-posts.component";

import { provideMarkdown } from 'ngx-markdown';
import { MarkdownModule } from 'ngx-markdown';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ForumCategoriesComponent,
    ForumThreadsComponent,
    ForumPostsComponent,
    ConvertToReadableDate,
    LimitStringLength,
    ForumPostCardComponent,
    LookupMemberComponent,
    LookupThreadComponent,
    LookupPostComponent,
    SearchComponent,
    SearchThreadsComponent,
    SearchPostsComponent,
    SearchMembersComponent,
    ForumThreadListItemComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    MarkdownModule.forRoot(),
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'categories', component: ForumCategoriesComponent},
      {path: 'categories/:id', component: ForumThreadsComponent},
      {path: 'threads/:id', component: ForumPostsComponent},
      {path: 'search/:query', component: SearchComponent},
      {path: 'search/threads/:query', component: SearchThreadsComponent},
      {path: 'search/posts/:query', component: SearchPostsComponent},
      {path: 'search/members/:query', component: SearchMembersComponent}
      // {path: ''}
      // { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },

    ]),
    ReactiveFormsModule,
    ForumCategoryListItemComponent,
    ForumThreadListNewestSoftDeletedComponent,
    ForumThreadListNormalIconsComponent,
    ForumThreadListNoPostsComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

