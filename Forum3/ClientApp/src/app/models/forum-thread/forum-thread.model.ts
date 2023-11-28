import {ForumCategory} from "../forum-category.model";
import {LookupMember} from "../lookup/lookup-member.model";
import {LookupPost} from "../lookup/lookup-post.model";

export interface ForumThread {
  id: number;
  title: string;
  categoryId: number;
  creatorId: string;
  createdAt: Date;
  editedAt: Date;
  editedBy: string;
  isSoftDeleted: boolean;
  isPinned: boolean;
  isLocked: boolean;

  category: ForumCategory;
  latestPost: LookupPost;
  creator: LookupMember;
  postCount: number;
}
