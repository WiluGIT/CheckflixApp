import Table from "@/components/Table/Table";

const Admin = () => {
    return (
        <div className="admin">
            Admin page
            <div className="flex justify-center items-center text-white">
                <div className="card flex-1 shadow-2xl bg-base-100 overflow-auto">
                    <div className="card-body">
                        <Table />
                    </div>
                </div>
            </div>

        </div>
    );
};

export default Admin;