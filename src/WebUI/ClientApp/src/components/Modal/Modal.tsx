import cn from "classnames";

export type ModalPropsType = {
    children?: React.ReactNode;
    open: boolean;
    enableClickOutside?: boolean;
    onClose(): void;
};

const Modal: React.FC<ModalPropsType> = ({ children, open, enableClickOutside, onClose }) => {
    const modalClass = cn({
        "modal modal-bottom sm:modal-middle": true,
        "modal-open": open,
    });

    return (
        <div>
            <input type="checkbox" id="following-modal" className="modal-toggle" />
            <div className={modalClass}>
                <div className="modal-box">
                    {children}
                </div>
                {enableClickOutside && <label className="modal-backdrop" onClick={onClose}>Close</label>}
            </div>
        </div>
    );
};

export default Modal;
