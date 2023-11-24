export interface ForumPost{
  id: number;
  threadId: number
  creatorId: string
  content: string
  createdAt: Date
  editedAt: Date
  isSoftDeleted: boolean
  editedBy:string
}
