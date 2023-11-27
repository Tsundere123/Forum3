import {LookupMember} from "../lookup/lookupMember.model";
import {LookupPost} from "../lookup/lookupPost.model";

export interface ProfilePostsModel {
  user: LookupMember;
  posts: LookupPost[];
}
