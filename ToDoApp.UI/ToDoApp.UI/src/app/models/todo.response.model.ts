import { Todo } from "./todo.model";

export interface TodoResponse {
    items: Todo[];
    totalCount: number;
}