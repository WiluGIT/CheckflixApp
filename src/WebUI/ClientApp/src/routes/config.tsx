import { FC, ReactElement } from 'react';
import PrivateRoute from './PrivateRoute';

export type WrapperRouteProps = {
    children: ReactElement;
    auth?: boolean;
}

const WrapperRouteComponent: FC<WrapperRouteProps> = ({ auth, children }) => {
    if (auth) {
        return <PrivateRoute>{children}</PrivateRoute>;
    }
    return <>{children}</>;
};

export default WrapperRouteComponent;
