

interface IUserView extends IUpdatableView {
    userName: string;
    firstName: string;
    lastName: string;
    fullName: string;
    mobile: string;
    isActive: boolean;
    lockoutEnd: Date;
    email: string;
    gender: Gender;
    birthDate: Date;
    lastLoggedInTime: Date;
    
}
