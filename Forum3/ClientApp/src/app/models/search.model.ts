import {LookupMember} from "./lookup/lookup-member.model";
import {LookupThread} from "./lookup/lookup-thread.model";
import {LookupPost} from "./lookup/lookup-post.model";

export interface Search {
  threads: LookupThread[];
  posts: LookupPost[];
  members: LookupMember[];
}
