import {LookupThread} from "./lookup/lookup-thread.model";
import {LookupPost} from "./lookup/lookup-post.model";

export interface ForumCategory {
  id: number;
  name: string;
  description: string;
  latestThread?: LookupThread;
  threadCount: number;
  postCount: number;
}
