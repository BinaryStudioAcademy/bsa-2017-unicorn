export class RoleRouter {
    public getRouteByRole(roleId) {
        let path = 'index';
        switch (roleId) {
            case 2:
                path = 'search';
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
