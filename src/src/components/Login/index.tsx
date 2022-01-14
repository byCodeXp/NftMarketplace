import { Link } from 'react-router-dom';

export const Login = () => {
    return (
        <div className="container">
            <div className="row">
                <div className="col-4 offset-4">
                    <div className="form-group">
                        <label
                            htmlFor="inputUserName"
                            className="form-label mt-4"
                        >
                            Username
                        </label>
                        <input
                            type="text"
                            className="form-control"
                            id="inputUserName"
                            // aria-describedby="emailHelp"
                            placeholder="Enter username"
                        />
                        {/* <small id="emailHelp" className="form-text text-muted">
                            We'll never share your email with anyone else.
                        </small> */}
                    </div>
                </div>
                <div className="col-4 offset-4">
                    <div className="form-group">
                        <label
                            htmlFor="inputPassword"
                            className="form-label mt-4"
                        >
                            Password
                        </label>
                        <input
                            type="password"
                            className="form-control"
                            id="inputPassword"
                            placeholder="Password"
                        />
                    </div>
                </div>
                <div className="col-4 offset-4 mt-2">
                    <div className="form-check">
                        <input
                            className="form-check-input"
                            type="checkbox"
                            value=""
                            id="flexCheckChecked"
                        />
                        <label
                            className="form-check-label"
                            htmlFor="flexCheckChecked"
                        >
                            Remember me
                        </label>
                    </div>
                </div>
                <div className="col-4 offset-4 mt-5">
                    <button
                        type="submit"
                        className="btn btn-outline-dark col-12"
                    >
                        Login
                    </button>
                </div>
                <div className="col-4 offset-4 mt-2">
                    <Link to="/register">Register</Link>
                </div>
            </div>
        </div>
    );
};
