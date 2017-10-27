import { User } from "../login/User";

export class UserComment {

    Title: string;
    CommentText: string;
    Autor: string;
    PostTime: Date;

    constructor(title: string, commentText: string, autor: string, date: number) {
        this.Title = title;
        this.CommentText = commentText;
        this.Autor = autor;
        this.PostTime = new Date(date);
    }
}