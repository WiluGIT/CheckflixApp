import { type ReactElement } from 'react';
import { Navigate } from 'react-router';
//import useAuthStore from '@/store/useAuthStore';

interface Props {
    children: ReactElement;
}

const PrivateRoute: React.FC<Props> = ({ children }) => {
    // Replace with your auth condition
    //   const { isAuthenticated } = useAuthStore((state) => state);

    //   return isAuthenticated ? children : <Navigate to="/login" />;

    return children;
};

export default PrivateRoute;



// const PrivateRoute: FC<RouteProps> = ({children}) => {
//     const history = createBrowserHistory();
    
//       const [user, setUser] = useRecoilState(userState);
    
//       console.log('user: ', user);
//       const logged = user.username? true: false;
//       console.log('username: ', user.username, logged);
//       const navigate = useNavigate();
//       const { formatMessage } = useLocale();
//       const location = useLocation();
    
//       const { data: currentUser, error } = useGetCurrentUser();
    
//       useEffect(() => {
//         console.log("currentUser: ", currentUser);
//         setUser({ ...user, username: currentUser?.username || "", logged: true });
//       }, [currentUser]);
    
//       if (error) {
//         setUser({ ...user, logged: false });
//         return <Navigate to="/login" />
          
//       }
      
//       return logged ? (
//         <div>{children}</div>
//       ) : <Navigate to="/login" />
//     };
    
//     export default PrivateRoute;