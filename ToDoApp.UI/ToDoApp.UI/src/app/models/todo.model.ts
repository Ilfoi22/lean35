export interface Todo {
    id: string,
    title: string,
    description: string,
    category: string,
    createdDate: Date,
    isCompleted: boolean,
    completedDate: Date,
    isDeleted: boolean,
    deletedDate: Date,
    userId: string
};