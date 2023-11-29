import { LookupThread } from "./lookup/lookup-thread.model";

export interface ForumCategory {
  id: number;
  name: string;
  description: string;
  latestThread?: LookupThread;
  threadCount: number;
  postCount: number;
}
