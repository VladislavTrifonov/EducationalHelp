export default {
    props: ['model', 'validationStates'],
    methods: {
        getValidationState: function (propertyName) {
            return this.validationStates.filter(s => s.propertyName.toLowerCase() == propertyName).length == 0 ? null : false;
        }
    }
}