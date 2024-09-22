interface Author {
	id: string;
	name: string
}

export interface Post {
	id: string;
	title: string;
	content: string;
	author: Author;
	creationDate: Date;
}
