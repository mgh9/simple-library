var statics = {
    successMessage: 'Your request has been done successfully.',
    deleteQuestion: 'Do you want to delete this record?',
    editMessage: 'Your changes have been saved successfully',
    deleteMessage: 'The requested record[s] deleted successfully',
    icons: ['E84D', 'E84E', 'E914', 'E84F', 'E850', 'E851', 'E853', 'E854', 'E855', 'E856', 'E857', 'E858', 'E90B', 'E859', 'E85A', 'E85B', 'E85C', 'E85D', 'E85E', 'E85F', 'E860', 'E861', 'E862', 'E863', 'E864', 'E865', 'E866', 'E867', 'E868', 'E869', 'E86A', 'E8FC', 'E8F6', 'E8F7', 'E8F8', 'E86B', 'E86C', 'E86D', 'E86E', 'E86F', 'E915', 'E90C', 'E870', 'E871', 'E916', 'E872', 'E92B', 'E873', 'E875', 'E876', 'E877', 'E917', 'E918', 'E8FB', 'E926', 'E878', 'E903', 'E879', 'E87A', 'E87B', 'E87C', 'E87D', 'E87E', 'E87F', 'E880', 'E881', 'E90D', 'E904', 'E905', 'E882', 'E883', 'E927', 'E90E', 'E884', 'E908', 'E885', 'E886', 'E887', 'E8FD', 'E888', 'E889', 'E88A', 'E88B', 'E88C', 'E902', 'E88D', 'E912', 'E88E', 'E88F', 'E890', 'E891', 'E892', 'E893', 'E894', 'E895', 'E90F', 'E919', 'E91A', 'E896', 'E897', 'E898', 'E899', 'E89A', 'E89B', 'E91B', 'E89C', 'E90A', 'E91C', 'E89D', 'E89E', 'E89F', 'E8A0', 'E925', 'E8A1', 'E8A2', 'E8A3', 'E8A4', 'E8A5', 'E8A6', 'E8A7', 'E8A8', 'E8A9', 'E91D', 'E8AA', 'E911', 'E906', 'E8AB', 'E8AC', 'E91E', 'E8AD', 'E8AE', 'E8AF', 'E8B0', 'E91F', 'E8B1', 'E928', 'E8FE', 'E8B2', 'E8B3', 'E929', 'E8B4', 'E920', 'E921', 'E8B5', 'E8B6', 'E8B8', 'E8B9', 'E8BA', 'E8BB', 'E8BD', 'E8BC', 'E8BE', 'E8BF', 'E8C0', 'E8C1', 'E8C2', 'E8C3', 'E8C4', 'E8C5', 'E8C6', 'E8C7', 'E8C8', 'E8C9', 'E8CA', 'E8CB', 'E8CC', 'E8CD', 'E92A', 'E8CE', 'E8CF', 'E8D0', 'E8D1', 'E8D2', 'E8D3', 'E8D4', 'E8D5', 'E8D6', 'E8D7', 'E8D8', 'E8D9', 'E8DA', 'E8DB', 'E8DC', 'E8DD', 'E922', 'E8DE', 'E8DF', 'E8E0', 'E913', 'E8E1', 'E8E2', 'E8E3', 'E8E4', 'E8E5', 'E8E6', 'E8E7', 'E923', 'E8E8', 'E8E9', 'E8EA', 'E8EB', 'E8EC', 'E8ED', 'E8EE', 'E8EF', 'E8F0', 'E8F1', 'E8F2', 'E8F3', 'E8F4', 'E8F5', 'E924', 'E8F9', 'E8FA', 'E8FF', 'E900', 'E003', 'E000', 'E001', 'E002', 'E05C', 'E055', 'E019', 'E060', 'E01B', 'E06B', 'E06C', 'E01C', 'E01D', 'E01E', 'E01F', 'E020', 'E06D', 'E06E', 'E05D', 'E061', 'E05E', 'E06A', 'E062', 'E056', 'E057', 'E058', 'E021', 'E052', 'E023', 'E024', 'E02E', 'E02F', 'E030', 'E028', 'E029', 'E02A', 'E02B', 'E02C', 'E063', 'E031', 'E033', 'E06F', 'E034', 'E035', 'E036', 'E037', 'E038', 'E039', 'E03B', 'E065', 'E05F', 'E03C', 'E03D', 'E066', 'E03E', 'E03F', 'E067', 'E040', 'E041', 'E042', 'E059', 'E05A', 'E05B', 'E043', 'E044', 'E045', 'E068', 'E046', 'E053', 'E047', 'E064', 'E048', 'E049', 'E070', 'E071', 'E04A', 'E04B', 'E04C', 'E04D', 'E04E', 'E04F', 'E050', 'E051', 'E069', 'E0AF', 'E0B0', 'E0B1', 'E0B2', 'E0B3', 'E0B4', 'E0E4', 'E0B5', 'E0B6', 'E0B7', 'E0CA', 'E0CB', 'E0B8', 'E0B9', 'E0D0', 'E0CF', 'E0BA', 'E0BB', 'E0BC', 'E0BE', 'E0BF', 'E0E0', 'E0C3', 'E0C4', 'E0C6', 'E0C7', 'E0C8', 'E0E1', 'E0C9', 'E0CC', 'E0CD', 'E0DB', 'E0DC', 'E0DD', 'E0DE', 'E0CE', 'E0DF', 'E0D1', 'E0E5', 'E0E2', 'E0D2', 'E0D3', 'E0D4', 'E0D5', 'E0D6', 'E0E3', 'E0D7', 'E0D8', 'E0D9', 'E0DA', 'E145', 'E146', 'E147', 'E148', 'E149', 'E14A', 'E14B', 'E14C', 'E14D', 'E14E', 'E14F', 'E150', 'E16C', 'E151', 'E152', 'E153', 'E167', 'E154', 'E155', 'E156', 'E157', 'E16D', 'E158', 'E159', 'E168', 'E16A', 'E15A', 'E15B', 'E15C', 'E15D', 'E15E', 'E15F', 'E160', 'E161', 'E162', 'E163', 'E164', 'E165', 'E169', 'E166', 'E16B', 'E190', 'E191', 'E192', 'E193', 'E195', 'E194', 'E19C', 'E1A3', 'E1A4', 'E1A5', 'E1A6', 'E1A7', 'E1A8', 'E1A9', 'E1AA', 'E1AB', 'E1AC', 'E1AD', 'E1AE', 'E1AF', 'E1B0', 'E1B1', 'E1B2', 'E1B3', 'E1B4', 'E1B5', 'E1B8', 'E1B6', 'E1B7', 'E1B9', 'E1BA', 'E1BB', 'E1BE', 'E1BF', 'E1C0', 'E1C1', 'E1C2', 'E1C3', 'E1C8', 'E1CD', 'E1CE', 'E1CF', 'E1D0', 'E1D8', 'E1D9', 'E1DA', 'E1DB', 'E1E0', 'E1BC', 'E1BD', 'E1E1', 'E1E2', 'E226', 'E227', 'E228', 'E229', 'E22A', 'E22B', 'E22C', 'E22D', 'E22E', 'E22F', 'E230', 'E231', 'E232', 'E233', 'E6DD', 'E25D', 'E234', 'E235', 'E236', 'E237', 'E238', 'E239', 'E23A', 'E23B', 'E23C', 'E23D', 'E23E', 'E23F', 'E240', 'E241', 'E242', 'E243', 'E244', 'E25E', 'E245', 'E246', 'E247', 'E248', 'E249', 'E24A', 'E25F', 'E24B', 'E24C', 'E24D', 'E24E', 'E24F', 'E250', 'E251', 'E260', 'E252', 'E253', 'E254', 'E263', 'E25C', 'E6DF', 'E6C4', 'E6C5', 'E255', 'E261', 'E6E1', 'E256', 'E257', 'E262', 'E264', 'E258', 'E259', 'E25A', 'E25B', 'E2BC', 'E2BD', 'E2BE', 'E2BF', 'E2C0', 'E2C1', 'E2C2', 'E2C3', 'E2CC', 'E2C4', 'E2C6', 'E2C7', 'E2C8', 'E2C9', 'E307', 'E308', 'E30A', 'E30B', 'E30C', 'E30D', 'E335', 'E337', 'E30E', 'E30F', 'E310', 'E311', 'E312', 'E313', 'E314', 'E315', 'E316', 'E317', 'E318', 'E31A', 'E31B', 'E31C', 'E31D', 'E31E', 'E31F', 'E320', 'E321', 'E322', 'E323', 'E324', 'E325', 'E326', 'E327', 'E336', 'E328', 'E329', 'E32A', 'E32B', 'E32C', 'E32D', 'E32E', 'E32F', 'E330', 'E331', 'E332', 'E333', 'E338', 'E334', 'E439', 'E39D', 'E39E', 'E39F', 'E3A0', 'E3A1', 'E3A2', 'E3A3', 'E3A4', 'E3A5', 'E3A6', 'E3A7', 'E3A8', 'E3A9', 'E3AA', 'E3AB', 'E3AC', 'E3AD', 'E3AE', 'E43C', 'E3AF', 'E3B0', 'E3B1', 'E3B2', 'E3B3', 'E3B4', 'E3B5', 'E3B6', 'E431', 'E3B7', 'E3B8', 'E3B9', 'E3BA', 'E3BB', 'E3BE', 'E3BC', 'E3BD', 'E3BF', 'E3C0', 'E3C1', 'E3C2', 'E3C3', 'E3C4', 'E3C5', 'E437', 'E3C6', 'E3C7', 'E3C8', 'E3C9', 'E3CA', 'E3CB', 'E3CC', 'E3CD', 'E3CE', 'E3CF', 'E3D3', 'E3D0', 'E3D1', 'E3D2', 'E3D4', 'E3D5', 'E3D6', 'E3D7', 'E3D8', 'E3D9', 'E3DA', 'E3DB', 'E3DC', 'E3DD', 'E3DE', 'E3DF', 'E3E0', 'E3E2', 'E3E3', 'E3E4', 'E3E5', 'E3E6', 'E3E7', 'E3E8', 'E3E9', 'E3EA', 'E3EB', 'E3EC', 'E3ED', 'E3EE', 'E3F1', 'E3F2', 'E3F3', 'E3F4', 'E3F5', 'E3F6', 'E3F7', 'E3F8', 'E3F9', 'E3FA', 'E438', 'E3FC', 'E3FB', 'E3FD', 'E3FE', 'E3FF', 'E400', 'E401', 'E402', 'E403', 'E404', 'E43A', 'E405', 'E406', 'E407', 'E408', 'E409', 'E40A', 'E40B', 'E40C', 'E40D', 'E40E', 'E40F', 'E410', 'E411', 'E412', 'E43B', 'E413', 'E432', 'E433', 'E434', 'E415', 'E416', 'E417', 'E418', 'E419', 'E41A', 'E41B', 'E41C', 'E41D', 'E41E', 'E41F', 'E420', 'E421', 'E422', 'E425', 'E423', 'E424', 'E426', 'E427', 'E428', 'E429', 'E42A', 'E42B', 'E435', 'E42C', 'E42D', 'E42E', 'E436', 'E430', 'E567', 'E52D', 'E52E', 'E52F', 'E532', 'E530', 'E531', 'E534', 'E566', 'E533', 'E535', 'E536', 'E568', 'E56D', 'E539', 'E53A', 'E53B', 'E53C', 'E53F', 'E53D', 'E53E', 'E540', 'E541', 'E542', 'E543', 'E556', 'E544', 'E545', 'E546', 'E547', 'E548', 'E549', 'E54A', 'E54B', 'E54C', 'E54D', 'E54E', 'E54F', 'E550', 'E551', 'E552', 'E553', 'E554', 'E555', 'E557', 'E558', 'E559', 'E55B', 'E55C', 'E55D', 'E569', 'E55A', 'E56A', 'E55E', 'E55F', 'E560', 'E56C', 'E561', 'E562', 'E563', 'E56E', 'E56F', 'E564', 'E565', 'E570', 'E571', 'E572', 'E56B', 'E5C3', 'E5C4', 'E5DB', 'E5C5', 'E5C6', 'E5C7', 'E5C8', 'E5D8', 'E5C9', 'E5CA', 'E5CB', 'E5CC', 'E5CD', 'E5CE', 'E5CF', 'E5DC', 'E5D0', 'E5D1', 'E5DD', 'E5D2', 'E5D3', 'E5D4', 'E5D5', 'E5D9', 'E5DA', 'E60E', 'E630', 'E631', 'E632', 'E633', 'E634', 'E635', 'E636', 'E637', 'E60F', 'E638', 'E610', 'E612', 'E611', 'E643', 'E644', 'E613', 'E63F', 'E614', 'E615', 'E616', 'E617', 'E639', 'E618', 'E619', 'E640', 'E61A', 'E641', 'E63A', 'E63B', 'E61B', 'E61C', 'E61D', 'E61E', 'E61F', 'E620', 'E63C', 'E645', 'E623', 'E624', 'E625', 'E626', 'E627', 'E628', 'E629', 'E62A', 'E62B', 'E62C', 'E62D', 'E62E', 'E62F', 'E63D', 'E63E', 'EB3B', 'EB3C', 'EB3D', 'EB3E', 'EB3F', 'EB40', 'EB41', 'EB42', 'EB43', 'EB44', 'EB45', 'EB46', 'EB47', 'EB48', 'EB49', 'E642', 'EB4A', 'EB4B', 'EB4C', 'E7E9', 'E7EE', 'E7EF', 'E7F0', 'E7F1', 'E7F2', 'E7F3', 'E7F4', 'E7F7', 'E7F5', 'E7F6', 'E7F8', 'E7F9', 'E7FA', 'E7FB', 'E7FC', 'E7FD', 'E7FE', 'E7FF', 'E800', 'E801', 'E80B', 'E80C', 'E811', 'E812', 'E813', 'E814', 'E815', 'E80D', 'E80E', 'E834', 'E835', 'E909', 'E837', 'E836', 'E838', 'E83A', 'E839']
};
//# sourceMappingURL=statics.js.map