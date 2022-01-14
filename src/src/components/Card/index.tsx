const Card = () => {
    return (
        <div className="col-4 mt-5">
            <div className="card text-white border border-black black">
                <div className="card-body">
                    <h4 className="card-title">Card title</h4>
                    <div>
                    <img className="w-100" src="https://img.kapital.kz/2wyYn-W4r_M/czM6Ly9rYXBpdGFsLXN0YXRpYy9pbWcvNy9kLzcvMC8zL2I2YjdkNTUzOTkwOGQ3ZDU4ZDg5OWM1YmVhMy5qcGc" alt="img"></img>
                </div>
                    <p className="card-text">
                        Some quick example text to build on the card title and
                        make up the bulk of the card's content.
                    </p>
                    <a href="#" className="card-link">
                        Card link
                    </a>
                    <a href="#" className="card-link">
                        Another link
                    </a>
                </div>
            </div>
        </div>
    );
};


export default Card;