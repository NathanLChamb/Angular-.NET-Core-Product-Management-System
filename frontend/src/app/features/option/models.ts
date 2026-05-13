export interface ReadOptionDto {
    id: number;
    name: string;
    optionValues: ReadOptionValueDto[];
}

export interface ReadOptionValueDto {
    id: number;
    value: string;
}

export interface CreateOptionDto {
    name: string;
    optionValues: string[]
}

export interface UpdateOptionDto {
    name: string;
    optionValues: UpdateOptionValueDto[]
}

export interface UpdateOptionValueDto {
    id?: number;
    value: string;
}