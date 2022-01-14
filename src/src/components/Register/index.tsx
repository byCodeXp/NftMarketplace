import { Link } from "react-router-dom";

export const Register = () => {
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
                <div className="col-4 offset-4 mt-5">
                <Link to="/login" className="text-decoration-none"><button type="button" className="btn btn-outline-dark col-5">
                       Login instead</button></Link>
                    <button type="submit" className="btn btn-outline-dark col-5 offset-2">Register</button>
                </div>
            </div>
        </div>
    );
};
