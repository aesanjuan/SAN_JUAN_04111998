export class Video {
    id: number = 0;
    title: string = '';
    description: string = '';
    category: number = 1;
    thumbnailFilePath: string = '';
    videoFilePath: string = '';
    isDeleted: boolean = false;
    uploadDateTime: Date = new Date();
    lastUpdatedDateTime: Date = new Date();
    imgSourceLink: string = '';
}

export interface Video {
    id: number,
    title: string,
    description: string,
    category: number,
    thumbnailFilePath: string,
    videoFilePath: string,
    isDeleted: boolean,
    uploadDateTime: Date,
    lastUpdatedDateTime: Date,
}