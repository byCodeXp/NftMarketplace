import { Link } from 'react-router-dom';
import Card from '../Card';

const Home = () => {
    return (
        <div className="row">
            <div className="col-8 offset-1">
                <input
                    className="form-control border border-dark"
                    type="text"
                    placeholder="Search"
                />
            </div>
            <div className="col-3">
                <li className="nav-item dropdown list-unstyled">
                    <a
                        className="nav-link dropdown-toggle"
                        data-bs-toggle="dropdown"
                        href="#"
                        role="button"
                        aria-haspopup="true"
                        aria-expanded="false"
                    >
                        Filter
                    </a>
                    <div className="dropdown-menu">
                        <a className="dropdown-item" href="#">
                            Action
                        </a>
                        <a className="dropdown-item" href="#">
                            Another action
                        </a>
                        <a className="dropdown-item" href="#">
                            Something else here
                        </a>
                        <div className="dropdown-divider"></div>
                        <a className="dropdown-item" href="#">
                            Separated link
                        </a>
                    </div>
                </li>
            </div>
            <div className="row">
                <Card></Card>
                <Card></Card>
                <Card></Card>
                <Card></Card>
                <Card></Card>
                <Card></Card>
                <Card></Card>
            </div>
            <div className='row'>
                <ul className="pagination col-6 offset-5">
                    <li className="page-item disabled">
                        <Link className="page-link" to="/">&laquo;</Link>
                    </li>
                    <li className="page-item active">
                        <Link className="page-link" to="/">1</Link>
                    </li>
                    <li className="page-item">
                        <Link className="page-link" to="/">2</Link>
                    </li>
                    <li className="page-item">
                        <Link className="page-link" to="/">3</Link>
                    </li>
                    <li className="page-item disabled">
                        <Link className="page-link" to="/">&raquo;</Link>
                    </li>
                </ul>
            </div>
        </div>
    );
};

export default Home;
