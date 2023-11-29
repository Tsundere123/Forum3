import {LookupMember} from "../lookup/lookup-member.model";
import {LookupPost} from "../lookup/lookup-post.model";

export interface ProfilePostsModel {
  user: LookupMember;
  posts: LookupPost[];
}
