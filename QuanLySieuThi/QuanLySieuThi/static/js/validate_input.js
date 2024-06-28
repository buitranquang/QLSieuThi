
//Đối tượng validator(hàm khởi tạo)
function Validator(option) {

    function getParent(element, selector) {
        while (element.parentElement) {
            if (element.parentElement.matches(selector)) {
                return element.parentElement;
            }
            element = element.parentElement;
        }
    }
    var selectorRules = {};
    //hàm thực hiện validate
    function validate(inputElement, rule) {

        var errorElement = getParent(inputElement, option.formGroupSelector).querySelector(option.errorSelector);
        var errorMessage;
        //Lấy ra các rule của selector
        var rules = selectorRules[rule.selector];
        //Lặp qua từng rule kiểm tra
        for (var i = 0; i < rules.length; i++) {
            switch (inputElement.type) {
                case 'radio':
                    errorMessage = rules[i](formValidator.querySelector(rule.selector + ':checked'));
                    break;
                case 'checkbox':
                    if (!input.matches(':checked')) {
                        values[input.name] = '';
                        return values;
                    }

                    if (!Array.isArray(values[input.name])) {
                        values[input.name] = [];
                    }
                    values[input.name].push(input.value);
                    break;
                case 'file':
                    values[input.name] = input.files;
                default:
                    errorMessage = rules[i](inputElement.value);
            }

            //Nếu có lỗi thù dừng kiểm tra
            if (errorMessage)
                break;
        }
        if (errorMessage) {
            errorElement.innerText = errorMessage;
            getParent(inputElement, option.formGroupSelector).classList.add('invalid');
            inputElement.classList.add('input-error');
        }
        else {
            errorElement.innerText = '';
            getParent(inputElement, option.formGroupSelector).classList.remove('invalid');
            inputElement.classList.remove('input-error');
        }
        return !errorMessage;
    }
    //lấy Element của form cần validate
    var formValidator = document.querySelector(option.form);
    if (formValidator) {
        formValidator.onsubmit = function (e) {
            e.preventDefault();
            var isFormValid = true;
            //lặp qua từng rule và validate tất cả
            option.rules.forEach(function (rule) {
                var inputElement = formValidator.querySelector(rule.selector);
                var isValid = validate(inputElement, rule);
                if (!isValid) {
                    isFormValid = false;
                }

            });

            if (isFormValid) {
                //trường hợp submit với js
                if (typeof option.onSubmit == 'function') {
                    var enableInput = formValidator.querySelectorAll('[name]');
                    var formValue = Array.from(enableInput).reduce(function (values, input) {

                        switch (input.type) {
                            case 'radio':
                            case 'checkbox':
                                if (input.matches(':checked')) {
                                    values[input.name] = formValidator.querySelector('input["name=' + input.name + '"]:checked').value;
                                }
                                else
                                    values[input.name] = "";
                                break;

                            default:
                                values[input.name] = input.value
                        }
                        return values;
                    }, {});
                    option.onSubmit(formValue)
                } else {
                    formValidator.submit();
                }
            }


        }
        option.rules.forEach(function (rule) {
            //lưu lại rule cho mỗi input
            if (Array.isArray(selectorRules[rule.selector])) {
                selectorRules[rule.selector].push(rule.test)
            } else {
                selectorRules[rule.selector] = [rule.test];
            }
            // 
            var inputElements = formValidator.querySelectorAll(rule.selector);
            Array.from(inputElements).forEach(function (inputElement) {
                inputElement.onblur = function () {
                    validate(inputElement, rule);
                }
                // Xử lý mỗi khi người dùng nhập vào input
                inputElement.oninput = function () {
                    var errorElement = getParent(inputElement, option.formGroupSelector).querySelector('.form-message');
                    errorElement.innerText = '';
                    getParent(inputElement, option.formGroupSelector).classList.remove('invalid');
                    inputElement.classList.remove('input-error');
                }
            });




        });


    }
}
//Định nghĩa các rules
//Nguyên tắt của các rule
//Khi có lỗi => trả ra lỗi
//khi không có lỗi => không trả ra gì cả
Validator.isRequired = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            return value ? undefined : message || "Vui lòng nhập trường hợp này";
        }
    }
}
Validator.isEmail = function (selector) {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value) ? undefined : "Vui lòng nhập Email";
        }
    }
}
Validator.minLength = function (selector, min) {
    return {
        selector: selector,
        test: function (value) {
            return value.trim().length >= min ? undefined : `Mật khẩu phải ít nhất ${min} kí tự`;
        }
    }
}
Validator.isConfirmed = function (selector, getConfirmvalue, message) {
    return {
        selector: selector,
        test: function (value) {
            return value === getConfirmvalue() ? undefined : message || "Giá trị nhập vào không chính xác";
        }
    }

}

Validator.isEqualPhone = function (selector, min) {
    return {
        selector: selector,
        test: function (value) {
            return value.trim().length == min ? undefined : `Số điện thoại phải đủ ${min} số`;
        }
    }
}

Validator.isDiscountPrice = function (selectorDiscount, selectorPrice, message) {
    return {
        selector: selectorDiscount,
        test: function (value) {
            const discount = parseFloat(document.querySelector(selectorDiscount).value);
            const price = parseFloat(document.querySelector(selectorPrice).value.replaceAll(",", "").replaceAll(".", ""));

            if (discount >= price || discount <= 0) {
                return message || 'Nhập đúng giá';
            }
            return undefined;
        }
    }
}