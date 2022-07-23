var dialogsOptions = {
    // selectors
    editDialogId: '#edit-dialog',
    singleViewDialogId: '#single-view-dialog',

    confirm: { bgclose: true, keyboard: true, labels: { 'Ok': 'Yes', 'Cancel': 'No' }, modal: false },
    delete: { bgclose: false, keyboard: false, labels: { 'Ok': 'Yes', 'Cancel': 'No' }, modal: false },

    //edit: { bgclose: true, keyboard: true, modal: true },
    //editNotClosable: { bgclose: false, keyboard: true, modal: true },

    closable: { bgclose: true, keyboard: true, modal: true },
    notClosable: { bgclose: false, keyboard: false, modal: true },
    nested: { bgclose: true, keyboard: true, modal: false },

    psDialog: '#ps-dialog',
};