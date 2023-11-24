import {LookupMember} from "./lookup/lookupMember.model";
import {LookupThread} from "./lookup/lookupThread.model";
import {LookupPost} from "./lookup/lookupPost.model";

export interface Home {
  threads: LookupThread[];
  posts: LookupPost[];
  members: LookupMember[];
}
