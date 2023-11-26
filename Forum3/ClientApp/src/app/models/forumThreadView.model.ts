import {ForumThread} from "./forumThread/forumThread.model";
import {ForumCategory} from "./forumCategory.model";
import {ForumPost} from "./forumPost.model";

export interface ForumThreadViewModel{
  forumCategory: ForumCategory;
  pinnedThreads: ForumThread[];
  forumThreads: ForumThread[];
  currentPage: number;
  totalPages: number;
  newestThread: ForumThread;
  newestPost: ForumPost;
}
