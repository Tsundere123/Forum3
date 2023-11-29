import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { MarkdownModule } from 'ngx-markdown';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';

import { ForumCategoriesComponent } from "./forum-categories/forum-categories.component";
import { ForumCategoryListItemComponent } from "./forum-categories/forum-category-list-item/forum-category-list-item.component";
import { ForumCategoryListItemNoThreadsInCategoryComponent } from "./forum-categories/forum-category-list-item/forum-category-list-item-no-threads-in-category/forum-category-list-item-no-threads-in-category.component";
import { ForumCategoryListItemNewestThreadComponent } from "./forum-categories/forum-category-list-item/forum-category-list-item-newest-thread/forum-category-list-item-newest-thread.component";

import { ForumThreadsComponent } from "./forum-threads/forum-threads.component";
import { ForumThreadListItemComponent } from "./forum-threads/forum-thread-list-item/forum-thread-list-item.component";
import { ForumThreadListNewestSoftDeletedComponent } from "./forum-threads/forum-thread-list-item/forum-thread-list-newest-soft-deleted/forum-thread-list-newest-soft-deleted.component";
import { ForumThreadListNormalIconsComponent } from "./forum-threads/forum-thread-list-item/forum-thread-list-normal-icons/forum-thread-list-normal-icons.component";
import { ForumThreadListNoPostsComponent } from "./forum-threads/forum-thread-list-item/forum-thread-list-no-posts/forum-thread-list-no-posts.component";
import { NewForumThreadComponent } from "./forum-threads/new-forum-thread/new-forum-thread.component";

import { ForumPostsComponent } from "./forum-posts/forum-posts.component";
import { ForumPostCardComponent } from "./forum-posts/forum-post-card/forum-post-card.component";
import { NewForumPostComponent } from "./forum-posts/new-forum-post/new-forum-post.component";

import { ConvertToReadableDate } from "./shared/convert-to-readable-date";
import { LimitStringLength } from "./shared/limit-string-length";

import { LookupMemberComponent } from "./shared/lookup/lookup-member/lookup-member.component";
import { LookupThreadComponent } from "./shared/lookup/lookup-thread/lookup-thread.component";
import { LookupPostComponent } from "./shared/lookup/lookup-post/lookup-post.component";

import { SearchComponent } from "./search/search.component";
import { SearchThreadsComponent } from "./search/search-threads/search-threads.component";
import { SearchPostsComponent } from "./search/search-posts/search-posts.component";
import { SearchMembersComponent } from "./search/search-members/search-members.component";

import { MemberListComponent } from "./member-list/member-list.component";
import { ProfileCardComponent } from "./shared/profile-card/profile-card.component";

import { LoadingContentComponent } from "./shared/loading-content/loading-content.component";
import { ErrorContentComponent } from "./shared/error-content/error-content.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,

    ForumCategoriesComponent,
    ForumCategoryListItemComponent,
    ForumCategoryListItemNoThreadsInCategoryComponent,
    ForumCategoryListItemNewestThreadComponent,

    ForumThreadsComponent,
    ForumThreadListItemComponent,
    ForumThreadListNormalIconsComponent,
    ForumThreadListNewestSoftDeletedComponent,
    ForumThreadListNoPostsComponent,
    NewForumThreadComponent,

    ForumPostsComponent,
    ForumPostCardComponent,
    NewForumPostComponent,

    ConvertToReadableDate,
    LimitStringLength,

    LookupMemberComponent,
    LookupThreadComponent,
    LookupPostComponent,

    SearchComponent,
    SearchThreadsComponent,
    SearchPostsComponent,
    SearchMembersComponent,

    MemberListComponent,
    ProfileCardComponent,

    LoadingContentComponent,
    ErrorContentComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    MarkdownModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'categories', component: ForumCategoriesComponent },
      { path: 'categories/:id', component: ForumThreadsComponent },
      { path: 'threads/:id', component: ForumPostsComponent },
      { path: 'threads/create/:id', component: NewForumThreadComponent, canActivate: [AuthorizeGuard] },
      { path: 'search/:query', component: SearchComponent },
      { path: 'search/threads/:query', component: SearchThreadsComponent },
      { path: 'search/posts/:query', component: SearchPostsComponent },
      { path: 'search/members/:query', component: SearchMembersComponent },
      { path: 'members', component: MemberListComponent },
    ]),
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  exports: [
    ConvertToReadableDate
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
