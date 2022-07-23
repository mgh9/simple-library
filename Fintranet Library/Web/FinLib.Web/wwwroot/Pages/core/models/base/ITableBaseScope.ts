interface ITableBaseScope<TEntityController, TEntity, TViewEntity extends IBaseView, TSearchFilter extends IBaseEntitySearchFilter>
    extends IBaseScope<TEntityController> {
    isTableLoading: boolean;
    isFilterActive: boolean,

    /** آیا اکشنی روی فرم جاری در حال اجراس؟ */
    isFormActionExecuting: boolean;

    /**
    * آیا در این فرم، تووش قابلیت نمایش گزارش وجود دارد؟
    */
    isReportabaleForm: boolean,

    /**
    * آیا در این فرم، قابلیت خروجی اکسل از گرید وجود دارد؟
    */
    isExcalableForm: boolean,

    /**
     * مدل جهت دریافت لیست موجودیت های فرم
     * شامل صفحه بندی و فیلتر جستجو
     * */
    request: IGetRequestDto<TSearchFilter>;

    /**
     * لیست موجودیت ها که از سمت سرور برگشته است
     * */
    tableData: ITableData<TEntity>;

    /**
     * کانفیگ گرید
     * */
    gridViewConfig: IGridViewConfig;

    /**
     * موجودیت جاری هنگامی که انتخاب میشه جهت یک اکشن - مثل ویرایش (که باعث میشه مدال ویرایش باز بشه)، یا 
     * کلیک بر روی کلید حذف روی گرید، که کاربر میخاد موجودیت رو حذف کنه
     * */
    entity: TEntity;
    originalEntity: TEntity;

    /**
     * موجودیت جهت مشاهده در فرم نمایش بصورت فقط خواندنی
     * */
    singleViewModel: TViewEntity;
}
