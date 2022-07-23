interface ICustomDialog {
    confirm: Function;
    reject: Function;
}

interface ICustomDialogData<T> {
    Data: T;
}
