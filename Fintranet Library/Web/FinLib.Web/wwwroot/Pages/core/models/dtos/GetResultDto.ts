




interface IGetResultDto <TView> extends IBaseDto 
{   
    count: number;
    data: TView[];
}