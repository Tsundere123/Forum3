import {ForumCategory} from "../forumCategory.model";
import {LookupMember} from "../lookup/lookupMember.model";
import {LookupPost} from "../lookup/lookupPost.model";

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
