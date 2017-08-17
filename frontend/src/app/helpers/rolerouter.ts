export class RoleRouter {
    public getRouteByRole(roleId: number) {
        let path = 'index';
        switch (roleId) {
            default:
            case 1:
                break;
            case 2:
                path = 'user';
                break;
            case 3:
                path = 'vendor';
                break;
            case 4:
                path = 'company';
                break;
            case 5:
                path = 'admin';
                break;
        }
        return path;
    }
}
