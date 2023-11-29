import { ForumThread } from "./forum-thread/forum-thread.model";
import { ForumCategory } from "./forum-category.model";
import { ForumPost } from "./forum-post.model";

export interface ForumThreadViewModel{
  forumCategory: ForumCategory;
  pinnedThreads: ForumThread[];
  forumThreads: ForumThread[];
  currentPage: number;
  totalPages: number;
  newestThread: ForumThread;
  newestPost: ForumPost;
}
