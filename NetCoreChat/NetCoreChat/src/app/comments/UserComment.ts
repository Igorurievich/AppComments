import { User } from "../login/User";

export class UserComment {

    Id: number;
    Title: string;
    Decsription: string;
    Autor: User;
    
    constructor(id:number, title:string, description:string, autor:User){
        this.Id = id;
        this.Title = title;
        this.Decsription = description;
        this.Autor = autor;
    }
}