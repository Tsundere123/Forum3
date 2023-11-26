import {LookupThread} from "./lookup/lookupThread.model";
import {LookupPost} from "./lookup/lookupPost.model";

export interface ForumCategory {
  id: number;
  name: string;
  description: string;
  latestThread: LookupThread;
  threadCount: number;
  postCount: number;
}
