import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import {ForumPostsComponent} from "./forumPosts/forumPosts.component";
import {ConvertToReadableDate} from "./shared/convertToReadableDate";
import {LimitStringLength} from "./shared/limitStringLength";
import {ForumPostCardComponent} from "./forumPosts/forum-post-card/forum-post-card.component";
import {LookupMemberComponent} from "./shared/lookup/lookup-member/lookupMember.component";
import {LookupThreadComponent} from "./shared/lookup/lookup-threads/lookupThread.component";
import {LookupPostComponent} from "./shared/lookup/lookup-post/lookupPost.component";

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
    LookupPostComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'categories', component: ForumCategoriesComponent },
      { path: 'categories/:id', component: ForumThreadsComponent },
      { path: 'threads/:id', component: ForumPostsComponent }
      // {path: ''}
      // { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },

    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
